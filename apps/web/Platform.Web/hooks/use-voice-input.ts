"use client";

import { useState, useEffect, useRef, useCallback } from "react";

interface VoiceInputOptions {
  onTranscript: (text: string) => void;
  onError?: (error: string) => void;
  language?: string;
  continuous?: boolean;
  interimResults?: boolean;
}

interface VoiceInputReturn {
  isRecording: boolean;
  isSupported: boolean;
  error: string | null;
  startRecording: () => void;
  stopRecording: () => void;
  toggleRecording: () => void;
}

declare global {
  interface Window {
    SpeechRecognition: any;
    webkitSpeechRecognition: any;
  }
}

export function useVoiceInput({
  onTranscript,
  onError,
  language = "en-US",
  continuous = true,
  interimResults = true,
}: VoiceInputOptions): VoiceInputReturn {
  const [isRecording, setIsRecording] = useState(false);
  const [isSupported, setIsSupported] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const recognitionRef = useRef<any>(null);

  // Check browser compatibility on mount
  useEffect(() => {
    const SpeechRecognition =
      typeof window !== "undefined" &&
      (window.SpeechRecognition || window.webkitSpeechRecognition);

    if (SpeechRecognition) {
      setIsSupported(true);
      recognitionRef.current = new SpeechRecognition();
      recognitionRef.current.continuous = continuous;
      recognitionRef.current.interimResults = interimResults;
      recognitionRef.current.lang = language;

      // Handle speech recognition results
      recognitionRef.current.onresult = (event: any) => {
        let transcript = "";
        for (let i = event.resultIndex; i < event.results.length; i++) {
          const result = event.results[i];
          if (result.isFinal) {
            transcript += result[0].transcript;
          }
        }

        if (transcript) {
          onTranscript(transcript);
        }
      };

      // Handle errors
      recognitionRef.current.onerror = (event: any) => {
        console.error("Speech recognition error:", event.error);
        
        let errorMessage = "An error occurred with speech recognition.";
        
        switch (event.error) {
          case "not-allowed":
          case "permission-denied":
            errorMessage = "Microphone access denied. Please enable microphone permissions.";
            break;
          case "no-speech":
            errorMessage = "No speech detected. Please try again.";
            break;
          case "audio-capture":
            errorMessage = "No microphone found. Please check your audio devices.";
            break;
          case "network":
            errorMessage = "Network error. Please check your connection.";
            break;
          case "aborted":
            // User stopped recording, not an error
            return;
        }

        setError(errorMessage);
        if (onError) {
          onError(errorMessage);
        }
        setIsRecording(false);
      };

      // Handle end of speech recognition
      recognitionRef.current.onend = () => {
        setIsRecording(false);
      };
    } else {
      setIsSupported(false);
      const errorMsg = "Speech recognition is not supported in this browser. Please use Chrome, Edge, or Safari.";
      setError(errorMsg);
      if (onError) {
        onError(errorMsg);
      }
    }

    // Cleanup on unmount
    return () => {
      if (recognitionRef.current) {
        try {
          recognitionRef.current.stop();
        } catch (e) {
          // Ignore errors on cleanup
        }
      }
    };
  }, [continuous, interimResults, language, onTranscript, onError]);

  const startRecording = useCallback(() => {
    if (!isSupported || !recognitionRef.current) {
      return;
    }

    try {
      setError(null);
      recognitionRef.current.start();
      setIsRecording(true);
    } catch (e: any) {
      // Recognition might already be started
      if (e.message && !e.message.includes("already started")) {
        const errorMsg = "Failed to start recording. Please try again.";
        setError(errorMsg);
        if (onError) {
          onError(errorMsg);
        }
      }
    }
  }, [isSupported, onError]);

  const stopRecording = useCallback(() => {
    if (!recognitionRef.current) {
      return;
    }

    try {
      recognitionRef.current.stop();
      setIsRecording(false);
    } catch (e) {
      // Ignore errors on stop
      setIsRecording(false);
    }
  }, []);

  const toggleRecording = useCallback(() => {
    if (isRecording) {
      stopRecording();
    } else {
      startRecording();
    }
  }, [isRecording, startRecording, stopRecording]);

  return {
    isRecording,
    isSupported,
    error,
    startRecording,
    stopRecording,
    toggleRecording,
  };
}

