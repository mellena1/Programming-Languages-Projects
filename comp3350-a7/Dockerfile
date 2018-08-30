# -------Build the code into a dll-------
FROM microsoft/dotnet:2.1-sdk as builder

# Copy the proj files
COPY comp3350-a7.sln .
RUN mkdir comp3350-a7
COPY comp3350-a7/comp3350-a7.csproj comp3350-a7/

# Restore project
RUN dotnet restore

# Copy the source files
COPY comp3350-a7/*.cs comp3350-a7/

# Build
WORKDIR comp3350-a7/
RUN dotnet publish -c Release -o release


# -------Runtime-------
FROM microsoft/dotnet:2.1-runtime as runner

# Copy from builder and run
COPY --from=builder /comp3350-a7/release ./
CMD ["dotnet", "comp3350-a7.dll"]
