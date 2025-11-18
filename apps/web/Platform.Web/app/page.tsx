import { Header } from "@/components/header";
import { auth0 } from "@/lib/auth0";
import { HomeClient } from "./home-client";
import { MarketingLanding } from "./marketing-landing";

export default async function HomePage() {
  // Get session on server (optional for home page - can be anonymous)
  const session = await auth0.getSession();

  // For logged-out users, show marketing landing page
  if (!session?.user) {
    return (
      <div className="min-h-screen bg-background">
        <Header user={null} />
        <MarketingLanding />
      </div>
    );
  }

  // For logged-in users, show the welcome home page
  return (
    <div className="min-h-screen bg-background">
      <Header user={session.user} />
      <HomeClient user={session.user} />
    </div>
  );
}
