# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file
COPY LibraryManagement.sln .

# Copy project files
COPY LibraryManagement/LibraryManagement.csproj LibraryManagement/
COPY LibraryManagement.Application/LibraryManagement.Application.csproj LibraryManagement.Application/
COPY LibraryManagement.Domain/LibraryManagement.Domain.csproj LibraryManagement.Domain/
COPY LibraryManagement.Infrastructure/LibraryManagement.Infrastructure.csproj LibraryManagement.Infrastructure/
COPY LibraryManagement.Shared/LibraryManagement.Shared.csproj LibraryManagement.Shared/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Publish API project
WORKDIR /app/LibraryManagement
RUN dotnet publish -c Release -o /out

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "LibraryManagement.dll"]
