pipeline {
    agent any

    stages {
        stage ('Checkout') {
            git branch: 'main', credentialsId: 'a1bddced-5054-4c5a-8445-a9ac6f5ba7ee', url: 'ssh://git@git.leuze.de:2022/rfid/test.git'
        }
        stage ('Build') {
            echo "building..."
            sleep 10
        }
    }
}