DOCKER_TAG = comp3350-a6

build:
	docker build -t $(DOCKER_TAG):latest .

run:
	make build
	docker run --rm -it $(DOCKER_TAG)
