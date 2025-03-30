import React from "react";
import {
  Grid,
  TextField,
  Button,
  Box,
  CircularProgress,
  SelectChangeEvent,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
} from "@mui/material";
import { Category } from "../../../types";

interface JokeControlsProps {
  category: string;
  numberOfJokes: string;
  categories: Category[] | null;
  isCategoriesLoading: boolean;
  isJokesLoading: boolean;
  onCategoryChange: (event: SelectChangeEvent) => void;
  onNumberChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
  onGenerateJokes: () => void;
}

// Control panel for the joke generator
// Allows selecting category, number of jokes, and generating jokes
const JokeControls: React.FC<JokeControlsProps> = ({
  category,
  numberOfJokes,
  categories,
  isCategoriesLoading,
  isJokesLoading,
  onCategoryChange,
  onNumberChange,
  onGenerateJokes,
}) => {
  return (
    <Grid container spacing={3}>
      <Grid item xs={12} md={6}>
        <FormControl fullWidth variant="outlined">
          <InputLabel id="category-label">Category</InputLabel>
          <Select
            labelId="category-label"
            id="category-select"
            value={category}
            onChange={onCategoryChange}
            label="Category"
            disabled={isCategoriesLoading}
          >
            {isCategoriesLoading ? (
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
      </Grid>

      <Grid item xs={12} md={6}>
        <TextField
          fullWidth
          label="Number of Jokes (1-9)"
          variant="outlined"
          type="number"
          inputProps={{ min: 1, max: 9 }}
          value={numberOfJokes}
          onChange={onNumberChange}
        />
      </Grid>

      <Grid item xs={12}>
        <Box sx={{ display: "flex", gap: 2 }}>
          <Button
            variant="contained"
            color="primary"
            onClick={onGenerateJokes}
            disabled={isJokesLoading}
            startIcon={isJokesLoading ? <CircularProgress size={20} /> : null}
          >
            {isJokesLoading ? "Generating..." : "Generate Jokes"}
          </Button>
        </Box>
      </Grid>
    </Grid>
  );
};

export default JokeControls;
