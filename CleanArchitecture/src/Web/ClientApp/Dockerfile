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

# Build the app for production
RUN npm run build

# # Install `serve` to run the application
# RUN npm install -g serve
# The port that your container will run on
#EXPOSE 44447
 
CMD ["npm", "start"]