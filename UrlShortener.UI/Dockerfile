FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

FROM nginx:alpine AS final
COPY --from=build /app/out/wwwroot /usr/share/nginx/html
EXPOSE 80
