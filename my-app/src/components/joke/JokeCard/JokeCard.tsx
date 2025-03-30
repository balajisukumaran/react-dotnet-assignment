import React from "react";
import { Card, CardContent, Typography, Tooltip } from "@mui/material";
import { Joke } from "../../../types";

interface JokeCardProps {
  joke: Joke;
}

// Card component that displays a single joke
// Hover over the joke text to see the icon URL as a tooltip
const JokeCard: React.FC<JokeCardProps> = ({ joke }) => {
  return (
    <Card
      sx={{
        height: "100%",
        display: "flex",
        flexDirection: "column",
        boxShadow: 2,
        borderRadius: 2,
        "&:hover": {
          boxShadow: 4,
        },
      }}
    >
      <CardContent
        sx={{
          flexGrow: 1,
          display: "flex",
          flexDirection: "column",
          justifyContent: "center",
          alignItems: "center",
          textAlign: "center",
          p: 3,
        }}
      >
        <Tooltip
          title={joke.iconUrl || "No image URL available"}
          arrow
          placement="top"
        >
          <Typography
            variant="body1"
            sx={{
              fontStyle: "italic",
              fontWeight: 500,
              fontSize: "1.1rem",
              lineHeight: 1.6,
            }}
          >
            {joke.value}
          </Typography>
        </Tooltip>
      </CardContent>
    </Card>
  );
};

export default JokeCard;
