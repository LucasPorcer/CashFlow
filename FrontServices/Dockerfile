#Arquivo de configuração para rodar a aplicação web via docker.

FROM node:18.16.0-alpine AS builder
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm install -g @angular/cli
RUN npm run build
CMD ["ng","serve","--host", "0.0.0.0"]