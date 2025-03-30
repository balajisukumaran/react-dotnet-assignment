import React from "react";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { SelectChangeEvent } from "@mui/material";
import CategorySelector from "./CategorySelector";

describe("CategorySelector", () => {
  const mockCategories = [
    { id: 1, name: "food" },
    { id: 2, name: "animal" },
  ];

  const mockOnChange = (event: SelectChangeEvent) => {};

  it("renders category options", async () => {
    render(
      <CategorySelector
        category="food"
        categories={mockCategories}
        isLoading={false}
        onChange={mockOnChange}
      />
    );

    const select = screen.getByRole("combobox", { name: /category/i });
    expect(select).toBeInTheDocument();

    await userEvent.click(select);

    expect(screen.getByRole("option", { name: /food/i })).toBeInTheDocument();
    expect(screen.getByRole("option", { name: /animal/i })).toBeInTheDocument();
  });
});
