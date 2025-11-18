import { auth0 } from '@/lib/auth0';
import { NextRequest, NextResponse } from 'next/server';

export async function GET(request: NextRequest) {
  try {
    return await auth0.middleware(request);
  } catch (error: any) {
    console.error('[Auth0 Callback Error]', {
      message: error?.message,
      code: error?.code,
      statusCode: error?.statusCode,
      error: error?.error,
      stack: error?.stack,
    });
    
    // Return a more helpful error page
    return new NextResponse(
      `Authentication Error: ${error?.message || 'Unknown error'}\n\n` +
      `Please check:\n` +
      `1. AUTH0_CLIENT_SECRET is set correctly in .env.local\n` +
      `2. Callback URL is configured in Auth0 Dashboard\n` +
      `3. Client ID matches your Auth0 Application\n\n` +
      `Check server logs for more details.`,
      { status: 500, headers: { 'Content-Type': 'text/plain' } }
    );
  }
}

