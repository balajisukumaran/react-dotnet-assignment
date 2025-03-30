import React from "react";
import {
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  SelectChangeEvent,
  CircularProgress,
} from "@mui/material";
import { Category } from "../../../types";

interface CategorySelectorProps {
  category: string;
  categories: Category[] | null;
  isLoading: boolean;
  onChange: (event: SelectChangeEvent) => void;
}

// Dropdown component for selecting joke categories
const CategorySelector: React.FC<CategorySelectorProps> = ({
  category,
  categories,
  isLoading,
  onChange,
}) => {
  return (
    <FormControl fullWidth variant="outlined">
      <InputLabel id="category-label">Category</InputLabel>
      <Select
        labelId="category-label"
        id="category-select"
        value={category}
        onChange={onChange}
        label="Category"
        disabled={isLoading}
      >
        {isLoading ? (
          <MenuItem value="">
            <CircularProgress size={20} /> Loading...
          </MenuItem>
        ) : (
          categories?.map((cat) => (
            <MenuItem key={cat.id} value={cat.name}>
              {cat.name}
            </MenuItem>
          ))
        )}
      </Select>
    </FormControl>
  );
};

export default CategorySelector;
