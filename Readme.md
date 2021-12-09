# GalacticAnnouncements

Your one-stop shop for announcements that are utterly otherworldly!

# Running locally

The only hard dependencies are docker and docker-compose. 

Tested versions: 
- docker (v20.10.11)
- docker-compose (v1.29.2)

## If you have gnu make
To run via Docker: 

`make run`.

To run the database on its own so you can run the api via other means (such as vscode/Rider/etc):

`make run-database`

## If you only have Docker

There is a known issue where the database may not start quickly enough if you just run `docker-compose up`.
Alas, I did not have time to address it.

This should work instead:
`docker-compose up db`
`docker-compose up api`

**In all cases, Swagger will be available at http://localhost:5070/swagger/index.html**
