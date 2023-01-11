FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TicTacToeGame.MVC/TicTacToeGame.MVC.csproj", "TicTacToeGame.MVC/"]
COPY ["TicTacToeGame.Application/TicTacToeGame.Application.csproj", "TicTacToeGame.Application/"]
COPY ["TicTacToeGame.Domain/TicTacToeGame.Domain.csproj", "TicTacToeGame.Domain/"]
COPY ["TicTacToeGame.Persistence/TicTacToeGame.Persistence.csproj", "TicTacToeGame.Persistence/"]
RUN dotnet restore "TicTacToeGame.MVC/TicTacToeGame.MVC.csproj"
RUN dotnet restore "TicTacToeGame.Application/TicTacToeGame.Application.csproj"
RUN dotnet restore "TicTacToeGame.Domain/TicTacToeGame.Domain.csproj"
RUN dotnet restore "TicTacToeGame.Persistence/TicTacToeGame.Persistence.csproj"
COPY . .
WORKDIR "/src/TicTacToeGame.MVC"
RUN dotnet build "TicTacToeGame.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicTacToeGame.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToeGame.MVC.dll"]
