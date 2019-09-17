FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FriendCircleAPI/FriendCircleAPI.csproj", "FriendCircleAPI/"]
RUN dotnet restore "FriendCircleAPI/FriendCircleAPI.csproj"
COPY . .
WORKDIR "/src/FriendCircleAPI"
RUN dotnet build "FriendCircleAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FriendCircleAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FriendCircleAPI.dll"]