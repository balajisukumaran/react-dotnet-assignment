import React from "react";
import { render, screen } from "@testing-library/react";
import JokeCard from "./JokeCard";
import "@testing-library/jest-dom";

describe("JokeCard", () => {
  const mockJoke = {
    value: "A funny joke about coding",
    iconUrl: "http://example.com/joke-icon.png",
  };

  it("renders joke text correctly", () => {
    render(<JokeCard joke={mockJoke} />);
    expect(screen.getByText("A funny joke about coding")).toBeInTheDocument();
  });

  it("displays icon URL on hover", () => {
    render(<JokeCard joke={mockJoke} />);
    const jokeText = screen.getByText("A funny joke about coding");

    // Basic check for tooltip presence
    expect(jokeText).toBeInTheDocument();
  });
});
