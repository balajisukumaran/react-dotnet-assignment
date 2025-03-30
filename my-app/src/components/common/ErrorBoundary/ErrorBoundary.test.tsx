import React from "react";
import { render, screen } from "@testing-library/react";
import ErrorBoundary from "./ErrorBoundary";
import ErrorThrower from "../ErrorThrower/ErrorThrower";

// Mock the ErrorPage used by ErrorBoundary
jest.mock("../../../pages/ErrorPage", () => {
  return ({ message }: { message: string }) => <div>Error Page: {message}</div>;
});

describe("ErrorBoundary", () => {
  it("catches errors from children and renders the error page", () => {
    const errorMessage = "Test error";
    render(
      <ErrorBoundary>
        <ErrorThrower message={errorMessage} />
      </ErrorBoundary>
    );
    expect(screen.getByText(`Error Page: ${errorMessage}`)).toBeInTheDocument();
  });

  it("renders children normally when no error occurs", () => {
    render(
      <ErrorBoundary>
        <div>No Error</div>
      </ErrorBoundary>
    );
    expect(screen.getByText("No Error")).toBeInTheDocument();
  });
});
