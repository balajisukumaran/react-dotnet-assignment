import axios from 'axios';
import { Category, Joke } from '../types';
import { getApiBaseUrl } from '../utils/environment';

// Create an axios instance with base configuration
const api = axios.create({
  baseURL: getApiBaseUrl(),
  timeout: 5000,
});

// Health check API call
// Returns true if API is available, false otherwise
export const checkApiHealth = async (): Promise<boolean> => {
  try {
    const response = await axios.get(`${getApiBaseUrl()}/health`, { 
      timeout: 3000 
    });
    return response.status === 200;
  } catch (error) {
    console.error('API health check failed:', error);
    return false;
  }
};

// Get joke categories from the API
export const getCategories = async (): Promise<Category[]> => {
  try {
    const response = await api.get('/api/jokes/categories');
    return response.data;
  } catch (error) {
    console.error('Failed to fetch categories:', error);
    throw error;
  }
};

// Get jokes by category and number
export const getJokes = async (category: string, number: string): Promise<Joke[]> => {
  try {
    const response = await api.get(`/api/jokes/${category}/${number}`);
    return response.data;
  } catch (error) {
    console.error('Failed to fetch jokes:', error);
    throw error;
  }
};