az acr login -n enigmatrytest
docker tag catch-registration-geoserver enigmatrytest.azurecr.io/waterschapshuis-catch-registration/dev/catch-registration-geoserver:latest
docker push enigmatrytest.azurecr.io/waterschapshuis-catch-registration/dev/catch-registration-geoserver:latest
docker tag enigmatrytest.azurecr.io/waterschapshuis-catch-registration/dev/catch-registration-geoserver:latest enigmatrytest.azurecr.io/waterschapshuis-catch-registration/dev/catch-registration-geoserver:2.19.0.0
docker push enigmatrytest.azurecr.io/waterschapshuis-catch-registration/dev/catch-registration-geoserver:2.19.0.0
