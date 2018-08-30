# -------Build the code into a dll-------
FROM microsoft/dotnet:2.1-sdk as builder

# Copy the proj files
COPY comp3350-lab8.sln .
RUN mkdir comp3350-lab8
COPY comp3350-lab8/comp3350-lab8.csproj comp3350-lab8/

# Restore project
RUN dotnet restore

# Copy the source files
COPY comp3350-lab8/*.cs comp3350-lab8/

# Build
WORKDIR comp3350-lab8/
RUN dotnet publish -c Release -o release


# -------Runtime-------
FROM microsoft/dotnet:2.1-runtime as runner

# Copy from builder and run
COPY --from=builder /comp3350-lab8/release ./
CMD ["dotnet", "comp3350-lab8.dll"]
