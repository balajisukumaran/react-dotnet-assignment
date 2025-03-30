import React from "react";
import { render, screen } from "@testing-library/react";
import JokeList from "./JokeList";
import "@testing-library/jest-dom";

describe("JokeList", () => {
  const mockJokes = [
    { value: "First joke", iconUrl: "http://example.com/joke1.png" },
    { value: "Second joke", iconUrl: "http://example.com/joke2.png" },
  ];

  it("renders multiple jokes", () => {
    render(<JokeList jokes={mockJokes} />);
    expect(screen.getByText("First joke")).toBeInTheDocument();
    expect(screen.getByText("Second joke")).toBeInTheDocument();
  });

  it("renders nothing when no jokes", () => {
    const { container } = render(<JokeList jokes={[]} />);
    expect(container.firstChild).toBeNull();
  });
});
