FROM ddas_bse

COPY --chown=appusr:appusr ./output/ /app/

WORKDIR /app
USER appusr
ENTRYPOINT [ "./ddrun" ]

