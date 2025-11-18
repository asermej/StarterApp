"use client";

import { Card } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { 
  Sparkles,
  Code2,
  FileCode,
  Database,
  Shield,
  Rocket,
  BookOpen,
  Github
} from "lucide-react";

interface HomeClientProps {
  user: any;
}

export function HomeClient({ user }: HomeClientProps) {
  const resources = [
    {
      icon: FileCode,
      title: ".cursor Rules",
      description: "AI-assisted development patterns and templates",
      link: "#",
      color: "from-blue-500 to-cyan-500"
    },
    {
      icon: Code2,
      title: "API Documentation",
      description: "Explore the working REST API examples",
      link: "/api/swagger",
      color: "from-purple-500 to-pink-500"
    },
    {
      icon: Database,
      title: "Database Schema",
      description: "Review Liquibase migrations and table structure",
      link: "#",
      color: "from-green-500 to-emerald-500"
    },
    {
      icon: Shield,
      title: "Auth0 Setup",
      description: "Authentication and authorization examples",
      link: "#",
      color: "from-orange-500 to-red-500"
    },
  ];

  const quickStart = [
    {
      title: "Explore the Architecture",
      items: [
        "Review the Clean Architecture implementation",
        "Understand the Domain, API, and Web layers",
        "Check out the Gateway pattern for external APIs",
      ]
    },
    {
      title: "Start Building",
      items: [
        "Use .cursor templates to generate new features",
        "Follow the interactive endpoint workflow",
        "Run acceptance tests to ensure quality",
      ]
    },
    {
      title: "Deploy",
      items: [
        "Configure your environment variables",
        "Set up PostgreSQL database",
        "Deploy to your preferred hosting platform",
      ]
    },
  ];

  // Get user initials for avatar
  const getInitials = () => {
    const email = user?.email || "";
    return email.substring(0, 2).toUpperCase();
  };

  return (
    <div className="min-h-screen bg-background">
      <div className="container mx-auto px-4 py-12 max-w-7xl">
        {/* Welcome Header */}
        <div className="mb-12 text-center">
          <div className="mb-6 inline-flex h-20 w-20 items-center justify-center rounded-full bg-gradient-to-br from-primary via-purple-500 to-pink-500 p-1">
            <div className="flex h-full w-full items-center justify-center rounded-full bg-background">
              <Sparkles className="h-10 w-10 text-primary" />
            </div>
          </div>

          <h1 className="mb-4 text-4xl font-bold md:text-5xl lg:text-6xl">
            Congratulations!{" "}
            <span className="bg-gradient-to-r from-primary via-purple-500 to-pink-500 bg-clip-text text-transparent">
              You're In.
            </span>
          </h1>

          <p className="mb-8 text-xl text-muted-foreground md:text-2xl">
            Now let's get to building something amazing.
          </p>

          {/* User Info Card */}
          <Card className="mx-auto inline-flex items-center gap-4 border-2 p-4">
            <Avatar className="h-12 w-12">
              <AvatarImage src={user?.picture} />
              <AvatarFallback className="bg-primary text-primary-foreground">
                {getInitials()}
              </AvatarFallback>
            </Avatar>
            <div className="text-left">
              <p className="font-semibold">{user?.name || user?.email}</p>
              <p className="text-sm text-muted-foreground">{user?.email}</p>
            </div>
          </Card>
        </div>

        {/* Resources Section */}
        <div className="mb-16">
          <h2 className="mb-6 text-2xl font-bold md:text-3xl">
            Resources & Documentation
          </h2>
          <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-4">
            {resources.map((resource) => (
              <Card
                key={resource.title}
                className="group cursor-pointer border-2 p-6 transition-all hover:border-primary/50 hover:shadow-lg"
              >
                <div className={`mb-4 inline-flex h-12 w-12 items-center justify-center rounded-lg bg-gradient-to-br ${resource.color}`}>
                  <resource.icon className="h-6 w-6 text-white" />
                </div>
                <h3 className="mb-2 font-semibold">{resource.title}</h3>
                <p className="text-sm text-muted-foreground">{resource.description}</p>
              </Card>
            ))}
          </div>
        </div>

        {/* Quick Start Guide */}
        <div className="mb-16">
          <h2 className="mb-6 text-2xl font-bold md:text-3xl">
            Quick Start Guide
          </h2>
          <div className="grid gap-6 md:grid-cols-3">
            {quickStart.map((section, index) => (
              <Card key={section.title} className="border-2 p-6">
                <div className="mb-4 flex items-center gap-3">
                  <div className="flex h-8 w-8 items-center justify-center rounded-full bg-primary text-sm font-bold text-primary-foreground">
                    {index + 1}
                  </div>
                  <h3 className="font-semibold">{section.title}</h3>
                </div>
                <ul className="space-y-2">
                  {section.items.map((item) => (
                    <li key={item} className="flex items-start gap-2 text-sm text-muted-foreground">
                      <div className="mt-1 h-1.5 w-1.5 flex-shrink-0 rounded-full bg-primary" />
                      {item}
                    </li>
                  ))}
                </ul>
              </Card>
            ))}
          </div>
        </div>

        {/* What's Next Section */}
        <Card className="border-2 bg-gradient-to-br from-primary/10 via-primary/5 to-background p-8 md:p-12">
          <div className="mx-auto max-w-3xl text-center">
            <Rocket className="mx-auto mb-4 h-12 w-12 text-primary" />
            <h2 className="mb-4 text-2xl font-bold md:text-3xl">
              What's Next?
            </h2>
            <p className="mb-6 text-lg text-muted-foreground">
              This starter application includes working examples of User management, 
              Chat functionality with AI gateway integration, and file uploads. 
              Use the .cursor rules and templates to add your own features following 
              the same patterns.
            </p>
            <div className="flex flex-col gap-4 sm:flex-row sm:justify-center">
              <Button size="lg" className="gap-2">
                <BookOpen className="h-5 w-5" />
                View Documentation
              </Button>
              <Button size="lg" variant="outline" className="gap-2">
                <Github className="h-5 w-5" />
                View on GitHub
              </Button>
            </div>
          </div>
        </Card>
      </div>
    </div>
  );
}
