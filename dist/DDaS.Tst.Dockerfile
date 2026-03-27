FROM ddas_bse

USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    curl \
    && rm -rf /var/lib/apt/lists/*

RUN netver=10.0.201 \
    && netarchive=dotnet-sdk-$netver-linux-x64.tar.gz \
    && curl --fail --show-error --location \
        --remote-name https://builds.dotnet.microsoft.com/dotnet/Sdk/$netver/${netarchive} \
    && mkdir -p /usr/share/dotnet \
    && tar --gzip --extract --no-same-owner --file ${netarchive} --directory /usr/share/dotnet \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    && rm ${netarchive} \
    && ln -s /home/appusr/.dotnet/tools/reportgenerator /usr/local/bin/reportgenerator

ENV DOTNET_GENERATE_ASPNET_CERTIFICATE=false \
    DOTNET_NOLOGO=true \
    NUGET_XMLDOC_MODE=skip

USER appusr
RUN dotnet tool install -g dotnet-reportgenerator-globaltool

COPY --chown=appusr:appusr ./tutput/ /app/
WORKDIR /app
ENTRYPOINT [ "./test.sh" ]

