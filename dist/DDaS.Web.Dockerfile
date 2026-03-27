FROM ddas_bse

COPY --chown=appusr:appusr ./output/ /app/

ENV ASPNETCORE_URLS=http://+:8080

WORKDIR /app
USER appusr
ENTRYPOINT [ "./DDaS.Server" ]

