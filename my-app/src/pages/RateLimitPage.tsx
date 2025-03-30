import React from 'react';
import { 
 Container, 
 Typography, 
 Button, 
 Box, 
 Paper 
} from '@mui/material';
import TimerOffIcon from '@mui/icons-material/TimerOff';
import { useNavigate } from 'react-router-dom';

// Page displayed when API rate limit is exceeded
// Informs user they need to wait before making more requests
const RateLimitPage: React.FC = () => {
 const navigate = useNavigate();

 return (
   <Container maxWidth="md">
     <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '100vh' }}>
       <Paper sx={{ p: 4, textAlign: 'center', maxWidth: '600px', width: '100%' }}>
         <TimerOffIcon color="warning" sx={{ fontSize: 64, mb: 2 }} />
         
         <Typography variant="h4" gutterBottom>
           Rate Limit Exceeded
         </Typography>
         
         <Typography variant="body1" sx={{ mb: 3 }}>
           You've sent too many requests to the joke API. Please wait a moment before trying again.
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

export default RateLimitPage;