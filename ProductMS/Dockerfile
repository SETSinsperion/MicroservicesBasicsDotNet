# We need to have a image from the microsoft-dotnet sdk image.
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# We create/set the work directory in the container.
WORKDIR /source

# Connect the localization we set in the web api project to docker.
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Copy the .csproj file to the project directory.
COPY ["ProductMS.csproj", "ProductMS/"]

# Restore the dependencies (libraries).
RUN dotnet restore "ProductMS/ProductMS.csproj"

# Copy all web api (MicroService) content
COPY . ./ProductMS

# Once we copied the project from local, let's change the work directory
# to the web api (MicroService). 
WORKDIR "/source/ProductMS"

# Now we build the project:
# -c: Flag to indicate the executable profile.
# -o: Destination for the executable. 
RUN dotnet build "ProductMS.csproj" -c Release -o /app/build

# Multistaging phase: publish.
# Using multiple FROM statements has the end to create temporary
# images that is used to the final image in the last FROM keyword.
FROM build AS publish

# Publishing the result.
RUN dotnet publish "ProductMS.csproj" -c Release -o /app/publish

# Final stage/image from publish stage. The final image should
# an entrypoint with the name "[.csproj filename].dll".
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductMS.dll"]