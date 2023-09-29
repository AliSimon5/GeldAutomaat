-- MySQL Script generated by MySQL Workbench
-- Wed Sep 13 13:08:33 2023
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema GeldAutomaat
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema GeldAutomaat
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `GeldAutomaat` DEFAULT CHARACTER SET utf8 ;
USE `GeldAutomaat` ;

-- -----------------------------------------------------
-- Table `GeldAutomaat`.`Rekeningen`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `GeldAutomaat`.`Rekeningen` (
  `RekeningNummer` VARCHAR(255) NOT NULL,
  `Pincode` VARCHAR(255) NOT NULL,
  `Saldo` INT NOT NULL,
  PRIMARY KEY (`RekeningNummer`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `GeldAutomaat`.`Medewerkers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `GeldAutomaat`.`Medewerkers` (
  `idMedewerkers` INT NOT NULL,
  `Gebruikersnaam` VARCHAR(255) NOT NULL,
  `Wachtwoord` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`idMedewerkers`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `GeldAutomaat`.`Transacties`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `GeldAutomaat`.`Transacties` (
  `TransactiesID` INT NOT NULL AUTO_INCREMENT,
  `Rekeningen_RekeningNummer` VARCHAR(255) NOT NULL,
  `Opnemen/Storten` TINYINT NOT NULL,
  `Tijd` DATETIME NOT NULL,
  `Hoeveel` VARCHAR(45) NOT NULL,
  `Medewerkers_idMedewerkers` INT NULL,
  PRIMARY KEY (`TransactiesID`),
  INDEX `fk_Transacties_Medewerkers1_idx` (`Medewerkers_idMedewerkers` ASC),
  INDEX `fk_Transacties_Rekeningen1_idx` (`Rekeningen_RekeningNummer` ASC),
  CONSTRAINT `fk_Transacties_Medewerkers1`
    FOREIGN KEY (`Medewerkers_idMedewerkers`)
    REFERENCES `GeldAutomaat`.`Medewerkers` (`idMedewerkers`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Transacties_Rekeningen1`
    FOREIGN KEY (`Rekeningen_RekeningNummer`)
    REFERENCES `GeldAutomaat`.`Rekeningen` (`RekeningNummer`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;