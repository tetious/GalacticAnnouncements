run:
	docker-compose up -d db
	sleep 2
	docker-compose up -d --build api

run-database:
	docker-compose up db

stop:
	docker-compose stop
