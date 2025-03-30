import React from "react";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter } from "react-router-dom";
import Home from "./Home";

// Mock the API calls
jest.mock("../../services/api", () => ({
  checkApiHealth: jest.fn().mockResolvedValue(true),
  getCategories: jest.fn().mockResolvedValue([
    { id: 1, name: "food" },
    { id: 2, name: "animal" },
  ]),
  getJokes: jest.fn().mockResolvedValue([]),
}));

describe("Home Page", () => {
  it("renders the page title", async () => {
    render(
      <MemoryRouter>
        <Home />
      </MemoryRouter>
    );

    // Wait for the heading to appear
    const heading = await screen.findByRole("heading", {
      name: /joke generator/i,
    });
    expect(heading).toBeInTheDocument();
  });

  it("loads categories and displays them in the select menu", async () => {
    render(
      <MemoryRouter>
        <Home />
      </MemoryRouter>
    );

    const categorySelect = await screen.findByRole("combobox", {
      name: /category/i,
    });
    expect(categorySelect).toBeInTheDocument();

    await userEvent.click(categorySelect);

    expect(
      await screen.findByRole("option", { name: /food/i })
    ).toBeInTheDocument();
    expect(
      await screen.findByRole("option", { name: /animal/i })
    ).toBeInTheDocument();
  });
});
