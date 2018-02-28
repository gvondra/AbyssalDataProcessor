interface AuthConfig {
    CLIENT_ID: string;
    CLIENT_DOMAIN: string;
    AUDIENCE: string;
    REDIRECT: string;
    SCOPE: string;
}

export const AUTH_CONFIG: AuthConfig = {
    CLIENT_ID: 'dZsTT8cN8UROjie0RmjpT9QCROyof3TY',
    CLIENT_DOMAIN: 'abyssaldataprocessor-dvlp.auth0.com', // e.g., you.auth0.com
    AUDIENCE: 'http://localhost/abyssaldataprocessor/api',
    REDIRECT: 'http://localhost:4200/callback',
    SCOPE: 'openid profile email'
  };