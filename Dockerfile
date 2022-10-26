FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/runtime:6.0

WORKDIR /app
COPY --from=builder /app/out .

ENTRYPOINT [ "/app/scanner2mqtt" ]