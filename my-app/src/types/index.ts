// Category model from the API
export interface Category {
  id: number;
  name: string;
}

// Joke model from the API
export interface Joke {
  iconUrl: string;
  value: string;
}

// API response for jokes
export interface JokesResponse {
  jokes: Joke[];
}