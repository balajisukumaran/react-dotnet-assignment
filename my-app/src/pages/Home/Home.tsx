import React, { useState, useEffect } from "react";
import {
  Container,
  Typography,
  Box,
  Paper,
  SelectChangeEvent,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import JokeControls from "../../components/joke/JokeControls/JokeControls";
import JokeList from "../../components/joke/JokeList/JokeList";
import Toast from "../../components/common/Toast/Toast";
import { getCategories, getJokes, checkApiHealth } from "../../services/api";
import { Joke, Category } from "../../types";
import ErrorThrower from "../../components/common/ErrorThrower/ErrorThrower";

// Main page component for the Joke Generator application
// Handles fetching joke categories, generating jokes, and checking API health
const Home: React.FC = () => {
  const navigate = useNavigate();

  // State
  const [category, setCategory] = useState<string>("any");
  const [numberOfJokes, setNumberOfJokes] = useState<string>("1");
  const [jokes, setJokes] = useState<Joke[]>([]);

  // Categories state
  const [categories, setCategories] = useState<Category[] | null>(null);
  const [categoriesLoading, setCategoriesLoading] = useState<boolean>(false);
  const [categoriesError, setCategoriesError] = useState<string | null>(null);

  // Health check state
  const [apiHealth, setApiHealth] = useState<{
    isChecked: boolean;
    isHealthy: boolean;
  }>({
    isChecked: false,
    isHealthy: true,
  });

  // Jokes loading state
  const [jokesLoading, setJokesLoading] = useState<boolean>(false);

  // Toast state
  const [showToast, setShowToast] = useState<boolean>(false);
  const [toastMessage, setToastMessage] = useState<string>("");

  // Check API health on mount
  useEffect(() => {
    const checkHealthAndLoadData = async () => {
      try {
        // First check if API is healthy
        const isHealthy = await checkApiHealth();

        setApiHealth({
          isChecked: true,
          isHealthy: isHealthy,
        });

        // If API is healthy, load categories
        if (isHealthy) {
          loadCategories();
        }
      } catch (error) {
        console.error("Health check error:", error);
        setApiHealth({
          isChecked: true,
          isHealthy: false,
        });
      }
    };

    checkHealthAndLoadData();
  }, []);

  // Handle errors by showing a toast
  useEffect(() => {
    if (categoriesError) {
      showErrorToast(categoriesError);
    }
  }, [categoriesError]);

  // Load joke categories from the API
  const loadCategories = async () => {
    setCategoriesLoading(true);
    setCategoriesError(null);

    try {
      const data = await getCategories();
      setCategories(data);
    } catch (error: any) {
      setCategoriesError("Failed to load categories");
      console.error("Failed to load categories:", error);

      // If we can't load categories, it's a fatal error
      if (error.response && error.response.status !== 429) {
        // Set API health to unhealthy to trigger the ErrorThrower
        setApiHealth({
          isChecked: true,
          isHealthy: false,
        });
      }
    } finally {
      setCategoriesLoading(false);
    }
  };

  // Handle category dropdown change
  const handleChangeCategory = (event: SelectChangeEvent) => {
    setCategory(event.target.value);
  };

  // Handle number of jokes input change
  const handleChangeNumber = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;
    if (/^[1-9]$/.test(value) || value === "") {
      setNumberOfJokes(value);
    }
  };

  // Handle generating jokes button click
  const handleGenerateJokes = async () => {
    if (!category || !numberOfJokes) {
      showErrorToast("Please select a category and number of jokes");
      return;
    }

    setJokesLoading(true);

    try {
      const response = await getJokes(category, numberOfJokes);
      setJokes(response);
    } catch (error: any) {
      // Handle rate limiting
      if (error.response && error.response.status === 429) {
        navigate("/rate-limit");
      } else {
        showErrorToast("Failed to load jokes");
        console.error("Failed to load jokes:", error);
      }
    } finally {
      setJokesLoading(false);
    }
  };

  // Show error toast message
  const showErrorToast = (message: string) => {
    setToastMessage(message);
    setShowToast(true);
  };

  // Handle closing the toast
  const handleCloseToast = () => {
    setShowToast(false);
  };

  // Throw error if API is not healthy
  if (apiHealth.isChecked && !apiHealth.isHealthy) {
    return (
      <ErrorThrower message="API is currently unavailable. Please try again later." />
    );
  }

  return (
    <Container maxWidth="lg">
      <Box sx={{ py: 4 }}>
        <Typography variant="h4" component="h1" gutterBottom>
          Joke Generator
        </Typography>

        <Paper elevation={3} sx={{ p: 3, mb: 4 }}>
          <Typography variant="body1" paragraph>
            This app generates random jokes. Provide a number of jokes (1-9) and
            select a category.
          </Typography>

          <JokeControls
            category={category}
            numberOfJokes={numberOfJokes}
            categories={categories}
            isCategoriesLoading={categoriesLoading}
            isJokesLoading={jokesLoading}
            onCategoryChange={handleChangeCategory}
            onNumberChange={handleChangeNumber}
            onGenerateJokes={handleGenerateJokes}
          />
        </Paper>

        <JokeList jokes={jokes} />
      </Box>

      {showToast && (
        <Toast
          message={toastMessage}
          severity="error"
          onClose={handleCloseToast}
        />
      )}
    </Container>
  );
};

export default Home;
