import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import JokeControls from "./JokeControls";
import { SelectChangeEvent } from "@mui/material";

describe("JokeControls", () => {
  const mockOnCategoryChange = jest.fn();
  const mockOnNumberChange = jest.fn();
  const mockOnGenerateJokes = jest.fn();
  const mockCategories = [
    { id: 1, name: "food" },
    { id: 2, name: "animal" },
  ];

  const defaultProps = {
    category: "food",
    numberOfJokes: "1",
    categories: mockCategories,
    isCategoriesLoading: false,
    isJokesLoading: false,
    onCategoryChange: mockOnCategoryChange,
    onNumberChange: mockOnNumberChange,
    onGenerateJokes: mockOnGenerateJokes,
  };

  beforeEach(() => {
    jest.clearAllMocks();
  });

  it("renders all controls", () => {
    render(<JokeControls {...defaultProps} />);
    expect(screen.getByLabelText(/category/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/number of jokes/i)).toBeInTheDocument();
    expect(
      screen.getByRole("button", { name: /generate jokes/i })
    ).toBeInTheDocument();
  });

  it("calls onCategoryChange when the category is changed", async () => {
    render(<JokeControls {...defaultProps} />);
    const select = screen.getByRole("combobox", { name: /category/i });
    await userEvent.click(select);
    const option = await screen.findByRole("option", { name: /animal/i });
    await userEvent.click(option);
    expect(mockOnCategoryChange).toHaveBeenCalled();
  });

  it("calls onNumberChange when the number input changes", () => {
    render(<JokeControls {...defaultProps} />);
    const input = screen.getByLabelText(/number of jokes/i);
    fireEvent.change(input, { target: { value: "3" } });
    expect(mockOnNumberChange).toHaveBeenCalled();
  });

  it("calls onGenerateJokes when the generate button is clicked", async () => {
    render(<JokeControls {...defaultProps} />);
    const button = screen.getByRole("button", { name: /generate jokes/i });
    await userEvent.click(button);
    expect(mockOnGenerateJokes).toHaveBeenCalled();
  });

  it("disables the generate button when jokes are loading", () => {
    render(<JokeControls {...defaultProps} isJokesLoading={true} />);
    // The button text changes when loading
    const button = screen.getByRole("button", { name: /generating/i });
    expect(button).toBeDisabled();
  });
});
