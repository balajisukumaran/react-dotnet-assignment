import { useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';

// Custom hook for handling API calls
// Manages loading states, error handling, and rate limiting
export function useApi<T>(apiFunction: (...args: any[]) => Promise<T>) {
 const [data, setData] = useState<T | null>(null);
 const [loading, setLoading] = useState(false);
 const [error, setError] = useState<string | null>(null);
 
 const navigate = useNavigate();

 const execute = useCallback(async (...args: any[]) => {
   setLoading(true);
   setError(null);
   
   try {
     const result = await apiFunction(...args);
     setData(result);
     return result;
   } catch (err: any) {
     // Check if it's a rate limit error
     if (err.response && err.response.status === 429) {
       navigate('/rate-limit');
     } else {
       const errorMessage = err.message || 'Something went wrong';
       setError(errorMessage);
       console.error('API Error:', err);
     }
     return null;
   } finally {
     setLoading(false);
   }
 }, [apiFunction, navigate]);

 return {
   data,
   loading,
   error,
   execute
 };
}