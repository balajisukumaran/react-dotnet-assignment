import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';

// global error handling
window.addEventListener('error', (event) => {
  console.error('Global error:', event.error);
});

// unhandled promise rejection handler
window.addEventListener('unhandledrejection', (event) => {
  console.error('Unhandled promise rejection:', event.reason);
});

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);