// Simple check for development environment
export const isDevelopment = process.env.NODE_ENV === "development";

// Get the API base URL from environment variables or use default
export const getApiBaseUrl = (): string => {
  return process.env.REACT_APP_API_BASE_URL || "https://localhost:7151";
};
