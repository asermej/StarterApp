"use client";

import { Button } from "@/components/ui/button";
import { Card } from "@/components/ui/card";
import { 
  Code2, 
  Shield, 
  Zap,
  ArrowRight,
  CheckCircle2,
  Database,
  Lock,
  Layers
} from "lucide-react";

export function MarketingLanding() {
  const features = [
    {
      icon: Layers,
      title: "Clean Architecture",
      description: "Built with separation of concerns, testability, and maintainability in mind. Domain-driven design with clear boundaries.",
      gradient: "from-blue-500 to-cyan-500",
    },
    {
      icon: Shield,
      title: "Auth0 Integration",
      description: "Production-ready authentication and authorization with JWT tokens, role-based access, and secure session management.",
      gradient: "from-purple-500 to-pink-500",
    },
    {
      icon: Code2,
      title: "Gateway Pattern",
      description: "Exemplary gateway architecture for external API integration, with OpenAI implementation as a working example.",
      gradient: "from-green-500 to-emerald-500",
    },
    {
      icon: Database,
      title: "Database Ready",
      description: "PostgreSQL with Liquibase migrations, Dapper for data access, and comprehensive CRUD operations.",
      gradient: "from-orange-500 to-red-500",
    },
    {
      icon: Zap,
      title: "API Examples",
      description: "Working REST APIs for User, Persona, Chat, Message, and Image uploads demonstrating best practices.",
      gradient: "from-yellow-500 to-orange-500",
    },
    {
      icon: Lock,
      title: "Security First",
      description: "Exception handling middleware, input validation, parameterized queries, and security best practices built-in.",
      gradient: "from-indigo-500 to-purple-500",
    },
  ];

  const techStack = [
    { name: "Next.js 14", category: "Frontend" },
    { name: ".NET 9", category: "Backend" },
    { name: "PostgreSQL", category: "Database" },
    { name: "Auth0", category: "Auth" },
    { name: "Tailwind CSS", category: "Styling" },
    { name: "TypeScript", category: "Language" },
  ];

  return (
    <div className="min-h-screen bg-background">
      {/* Hero Section */}
      <section className="relative overflow-hidden py-20 sm:py-32">
        <div className="absolute inset-0 bg-gradient-to-br from-primary/5 via-background to-primary/10" />
        
        <div className="container relative mx-auto px-4">
          <div className="mx-auto max-w-4xl text-center">
            {/* Badge */}
            <div className="mb-8 inline-flex items-center gap-2 rounded-full border bg-background/60 px-4 py-2 text-sm backdrop-blur-sm">
              <Zap className="h-4 w-4 text-primary" />
              <span className="font-medium">Enterprise Starter Application Template</span>
            </div>

            {/* Headline */}
            <h1 className="mb-6 text-5xl font-bold tracking-tight sm:text-6xl md:text-7xl">
              Start Building{" "}
              <span className="bg-gradient-to-r from-primary via-purple-500 to-pink-500 bg-clip-text text-transparent">
                Faster
              </span>
            </h1>

            {/* Subheadline */}
            <p className="mb-8 text-xl text-muted-foreground sm:text-2xl">
              A production-ready application template with Clean Architecture, Auth0, 
              Gateway patterns, and AI tooling built in.
            </p>

            {/* CTA Buttons */}
            <div className="flex flex-col gap-4 sm:flex-row sm:justify-center">
              <Button size="lg" asChild className="text-lg h-14 px-8">
                <a href="/api/auth/signup">
                  Get Started Free
                  <ArrowRight className="ml-2 h-5 w-5" />
                </a>
              </Button>
              <Button size="lg" variant="outline" asChild className="text-lg h-14 px-8">
                <a href="/api/auth/login">
                  Sign In
                </a>
              </Button>
            </div>

            {/* Tech Stack Pills */}
            <div className="mt-12 flex flex-wrap justify-center gap-3">
              {techStack.map((tech) => (
                <div
                  key={tech.name}
                  className="inline-flex items-center gap-2 rounded-full border bg-background/80 px-4 py-2 text-sm backdrop-blur-sm"
                >
                  <span className="font-semibold">{tech.name}</span>
                  <span className="text-muted-foreground">· {tech.category}</span>
                </div>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className="py-20">
        <div className="container mx-auto px-4">
          <div className="mx-auto max-w-6xl">
            {/* Section Header */}
            <div className="mb-16 text-center">
              <h2 className="mb-4 text-3xl font-bold md:text-4xl">
                Everything You Need to Start Building
              </h2>
              <p className="text-lg text-muted-foreground">
                Pre-configured architecture, authentication, and best practices
              </p>
            </div>

            {/* Features Grid */}
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
              {features.map((feature) => (
                <Card
                  key={feature.title}
                  className="group relative overflow-hidden border-2 p-6 transition-all hover:border-primary/50 hover:shadow-lg"
                >
                  <div className="relative z-10">
                    <div className={`mb-4 inline-flex h-12 w-12 items-center justify-center rounded-lg bg-gradient-to-br ${feature.gradient}`}>
                      <feature.icon className="h-6 w-6 text-white" />
                    </div>
                    <h3 className="mb-2 text-xl font-semibold">{feature.title}</h3>
                    <p className="text-muted-foreground">{feature.description}</p>
                  </div>
                </Card>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* What's Included Section */}
      <section className="bg-muted/30 py-20">
        <div className="container mx-auto px-4">
          <div className="mx-auto max-w-4xl">
            <div className="mb-12 text-center">
              <h2 className="mb-4 text-3xl font-bold md:text-4xl">
                What's Included
              </h2>
              <p className="text-lg text-muted-foreground">
                Ready-to-use components and working examples
              </p>
            </div>

            <div className="grid gap-4 md:grid-cols-2">
              {[
                "Complete Auth0 authentication setup",
                "User management with profile images",
                "Chat system with AI gateway integration",
                "File upload and storage handling",
                "PostgreSQL database with migrations",
                "Comprehensive exception handling",
                "Acceptance test patterns and utilities",
                "API documentation with Swagger",
                "Clean Architecture principles",
                "Domain-driven design patterns",
                "Cursor AI rules and templates",
                "Interactive development workflows",
              ].map((item) => (
                <div key={item} className="flex items-start gap-3">
                  <CheckCircle2 className="mt-0.5 h-5 w-5 flex-shrink-0 text-primary" />
                  <span className="text-muted-foreground">{item}</span>
                </div>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* CTA Section */}
      <section className="py-20">
        <div className="container mx-auto px-4">
          <div className="mx-auto max-w-4xl">
            <Card className="relative overflow-hidden border-2 border-primary/20 bg-gradient-to-br from-primary/10 via-primary/5 to-background p-12 text-center">
              <div className="relative space-y-6">
                <h2 className="text-3xl font-bold md:text-4xl">
                  Ready to Start Building?
                </h2>
                <p className="mx-auto max-w-2xl text-lg text-muted-foreground">
                  Sign up now and start with a solid foundation. All the architecture, 
                  authentication, and AI tooling you need.
                </p>

                <div className="flex flex-col gap-4 sm:flex-row sm:justify-center pt-4">
                  <Button size="lg" asChild className="text-lg h-14 px-8">
                    <a href="/api/auth/signup">
                      Get Started Free
                      <ArrowRight className="ml-2 h-5 w-5" />
                    </a>
                  </Button>
                  <Button size="lg" variant="outline" asChild className="text-lg h-14 px-8">
                    <a href="/api/auth/login">
                      Sign In
                    </a>
                  </Button>
                </div>

                <p className="text-sm text-muted-foreground pt-4">
                  No credit card required. Start building in seconds.
                </p>
              </div>
            </Card>
          </div>
        </div>
      </section>
    </div>
  );
}
