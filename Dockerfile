# Etapa 1: Build da aplicação React
FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Etapa 2: Servir os arquivos estáticos com NGINX
FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html

# Remove o default.conf e coloca um que redireciona corretamente
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
