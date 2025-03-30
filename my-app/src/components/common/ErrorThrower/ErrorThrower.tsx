import React from "react";

interface ErrorThrowerProps {
  message: string;
}

// This component throws an error when rendered
// It's used to trigger the ErrorBoundary
const ErrorThrower: React.FC<ErrorThrowerProps> = ({ message }) => {
  // Throw error during render so ErrorBoundary can catch it
  throw new Error(message);
};

export default ErrorThrower;
