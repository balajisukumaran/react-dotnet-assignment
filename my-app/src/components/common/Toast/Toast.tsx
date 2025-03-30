import React, { useState } from "react";
import { Alert, Snackbar } from "@mui/material";

interface ToastProps {
  message: string;
  severity?: "error" | "warning" | "info" | "success";
  onClose?: () => void;
}

// Simple toast notification component using Material UI
const Toast: React.FC<ToastProps> = ({
  message,
  severity = "error",
  onClose,
}) => {
  const [open, setOpen] = useState(true);

  // Handle closing the toast
  const handleClose = () => {
    setOpen(false);
    if (onClose) {
      onClose();
    }
  };

  return (
    <Snackbar
      open={open}
      autoHideDuration={5000}
      onClose={handleClose}
      anchorOrigin={{ vertical: "top", horizontal: "right" }}
    >
      <Alert onClose={handleClose} severity={severity}>
        {message}
      </Alert>
    </Snackbar>
  );
};

export default Toast;
