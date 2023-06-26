az acr login -n enigmatry
docker tag catch-registration-geoserver enigmatry.azurecr.io/waterschapshuis-catch-registration/acc/catch-registration-geoserver:latest
docker push enigmatry.azurecr.io/waterschapshuis-catch-registration/acc/catch-registration-geoserver:latest
docker tag enigmatry.azurecr.io/waterschapshuis-catch-registration/acc/catch-registration-geoserver:latest enigmatry.azurecr.io/waterschapshuis-catch-registration/acc/catch-registration-geoserver:2.19.0.0
docker push enigmatry.azurecr.io/waterschapshuis-catch-registration/acc/catch-registration-geoserver:2.19.0.0
