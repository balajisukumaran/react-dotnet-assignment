import React from 'react';
import { 
 Container, 
 Typography, 
 Button, 
 Box, 
 Paper 
} from '@mui/material';
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';
import { useNavigate } from 'react-router-dom';

interface ErrorPageProps {
 message?: string;
}

// Error page displayed when application encounters an error
// Shows friendly error message and provides way to return to home
const ErrorPage: React.FC<ErrorPageProps> = ({ 
 message = 'An unexpected error occurred'
}) => {
 const navigate = useNavigate();

 return (
   <Container maxWidth="md">
     <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '100vh' }}>
       <Paper sx={{ p: 4, textAlign: 'center', maxWidth: '600px', width: '100%' }}>
         <ErrorOutlineIcon color="error" sx={{ fontSize: 64, mb: 2 }} />
         
         <Typography variant="h4" gutterBottom>
           Something went wrong
         </Typography>
         
         <Typography variant="body1" sx={{ mb: 3 }}>
           {message}
         </Typography>
         
         <Button 
           variant="contained" 
           color="primary" 
           onClick={() => navigate('/')}
         >
           Back to Home
         </Button>
       </Paper>
     </Box>
   </Container>
 );
};

export default ErrorPage;