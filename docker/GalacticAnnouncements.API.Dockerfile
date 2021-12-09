FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /app

ARG WEB_PROJECT=GalacticAnnouncements.API

# Copy csproj and restore as distinct layers
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
RUN export RID=$(dotnet --info | grep RID | awk '{print $2}') && \
    dotnet restore src/${WEB_PROJECT} --use-lock-file -r $RID

# Copy everything else and build
COPY ./src ./src
RUN export RID=$(dotnet --info | grep RID | awk '{print $2}') && \
    dotnet publish src/${WEB_PROJECT} -c Release -o out -r $RID --self-contained true --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .

# Add libicu and disable Globalization Invariant Mode to prevent incorrect currency symbol in certain cases.
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
# Workaround for https://github.com/dotnet/aspnetcore/issues/38185
ENV ASPNETCORE_URLS=http://+:5070

ENTRYPOINT ["./GalacticAnnouncements.API"]
