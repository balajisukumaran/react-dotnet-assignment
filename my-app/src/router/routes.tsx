import { RouteObject } from "react-router-dom";
import Home from "../pages/Home/Home";
import ErrorPage from "../pages/ErrorPage";
import RateLimitPage from "../pages/RateLimitPage";
import ErrorBoundary from "../components/common/ErrorBoundary/ErrorBoundary";

// Application route configuration
// Wraps main page with ErrorBoundary to catch rendering errors
const routes: RouteObject[] = [
  {
    path: "/",
    element: (
      <ErrorBoundary>
        <Home />
      </ErrorBoundary>
    ),
  },
  {
    path: "/error",
    element: <ErrorPage />,
  },
  {
    path: "/rate-limit",
    element: <RateLimitPage />,
  },
  {
    path: "*",
    element: <ErrorPage message="Page not found" />,
  },
];

export default routes;
