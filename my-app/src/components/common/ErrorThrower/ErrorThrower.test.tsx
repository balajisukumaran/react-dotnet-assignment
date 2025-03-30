import React from "react";
import { render } from "@testing-library/react";
import ErrorThrower from "./ErrorThrower";

describe("ErrorThrower", () => {
  it("throws an error with the provided message", () => {
    const errorMessage = "Test error thrown";
    expect(() => render(<ErrorThrower message={errorMessage} />)).toThrow(
      errorMessage
    );
  });
});
