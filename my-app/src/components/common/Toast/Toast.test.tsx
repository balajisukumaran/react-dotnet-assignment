import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import Toast from "./Toast";

describe("Toast", () => {
  it("displays the toast message", () => {
    render(<Toast message="Test message" severity="success" />);
    expect(screen.getByText(/test message/i)).toBeInTheDocument();
  });

  it("calls onClose when the close button is clicked", async () => {
    const onCloseMock = jest.fn();

    render(<Toast message="Test message" onClose={onCloseMock} />);

    const closeButton = screen.getByRole("button", { name: /close/i });
    fireEvent.click(closeButton);

    await waitFor(() => {
      expect(onCloseMock).toHaveBeenCalled();
    });
  });
});
