import React from "react";
import { Grid, Typography, Box } from "@mui/material";
import JokeCard from "../JokeCard/JokeCard";
import { Joke } from "../../../types";

interface JokeListProps {
  jokes: Joke[];
}

// Component that displays a grid of joke cards
// Returns null if there are no jokes to display
const JokeList: React.FC<JokeListProps> = ({ jokes }) => {
  if (jokes.length === 0) {
    return null;
  }

  return (
    <Box sx={{ mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        Your Jokes
      </Typography>

      <Grid container spacing={3}>
        {jokes.map((joke, index) => (
          <Grid item xs={12} sm={6} md={4} key={index}>
            <JokeCard joke={joke} />
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};

export default JokeList;
