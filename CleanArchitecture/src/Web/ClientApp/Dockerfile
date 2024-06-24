#Use an official Node runtime as a parent image
FROM node:latest
 
# Set the working directory in the container
WORKDIR /app
 
# Copy the package.json and package-lock.json
COPY package*.json ./
 
# Install dependencies with legacy peer deps option
RUN npm install --f
 
# Bundle app source inside the Docker image
COPY . .
 
# Build the app for production# Use ARG to define the build-time environment variable
#ARG BUILD_ENV
#RUN case "$BUILD_ENV" in "ci") npm run build:ci ;; "qa") npm run build:qa ;; "staging") npm run build:staging --staging ;; "prod") npm run build:prod ;; *) npm run build ;; esac
 
#EXPOSE 44447

CMD ["npm", "start"]