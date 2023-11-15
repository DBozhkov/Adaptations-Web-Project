# Use an image that includes .NET Core SDK and Runtime for the build stage
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy the solution file and project files
COPY *.sln ./
COPY . ./

# Restore, build, and publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS runtime
WORKDIR /app

# Copy the published files from the build-env stage
COPY --from=build-env /app/publish .


# Specify the correct entry point for your application
CMD ["dotnet", "Adaptations.Web.dll"]
