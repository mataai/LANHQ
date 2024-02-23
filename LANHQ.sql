CREATE TABLE `users` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `display_name` TEXT,
  `email` TEXT UNIQUE NOT NULL,
  `firebase_id` TEXT UNIQUE NOT NULL
);

CREATE TABLE `roles` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `display_name` TEXT
);

CREATE TABLE `permissions` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `display_name` TEXT NOT NULL,
  `description` TEXT,
  `code` TEXT UNIQUE NOT NULL
);

CREATE TABLE `user_roles` (
  `user_id` INT NOT NULL,
  `role_id` INT NOT NULL,
  PRIMARY KEY (`user_id`, `role_id`)
);

CREATE TABLE `role_permissions` (
  `role_id` INT,
  `permission_id` INT,
  PRIMARY KEY (`role_id`, `permission_id`)
);

CREATE TABLE `lans` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `date_start` DATE NOT NULL,
  `date_end` DATE NOT NULL,
  `location` TEXT NOT NULL
);

CREATE TABLE `tickets` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `display_name` TEXT NOT NULL,
  `date_purchase` DATE NOT NULL,
  `lan_id` INT NOT NULL,
  `stripe_id` TEXT NOT NULL
);

CREATE TABLE `places` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `ticket_id` INT NOT NULL,
  `row` TEXT NOT NULL,
  `column` TEXT NOT NULL
);

CREATE TABLE `tournaments` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `game` TEXT NOT NULL,
  `date_start` DATE NOT NULL,
  `price_pool` TEXT,
  `lan_id` INT NOT NULL
);

CREATE TABLE `teams` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `display_name` TEXT NOT NULL,
  `leader_id` INT NOT NULL,
  `guild_id` INT
);

CREATE TABLE `guilds` (
  `id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `display_name` TEXT,
  `leader_id` INT NOT NULL
);

CREATE TABLE `tournament_registrations` (
  `registration_id` SERIAL PRIMARY KEY AUTO_INCREMENT,
  `tournament_id` INT NOT NULL,
  `participant_id` INT NOT NULL,
  `participant_type` TEXT NOT NULL
);

CREATE UNIQUE INDEX `tournament_registrations_index_0` ON `tournament_registrations` (`tournament_id`, `participant_id`, `participant_type`);

ALTER TABLE `user_roles` ADD FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

ALTER TABLE `user_roles` ADD FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`);

ALTER TABLE `role_permissions` ADD FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`);

ALTER TABLE `role_permissions` ADD FOREIGN KEY (`permission_id`) REFERENCES `permissions` (`id`);

ALTER TABLE `tickets` ADD FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

ALTER TABLE `tickets` ADD FOREIGN KEY (`lan_id`) REFERENCES `lans` (`id`);

ALTER TABLE `teams` ADD FOREIGN KEY (`guild_id`) REFERENCES `guilds` (`id`);

ALTER TABLE `tournaments` ADD FOREIGN KEY (`lan_id`) REFERENCES `lans` (`id`);

ALTER TABLE `places` ADD FOREIGN KEY (`ticket_id`) REFERENCES `tickets` (`id`);

ALTER TABLE `teams` ADD FOREIGN KEY (`leader_id`) REFERENCES `users` (`id`);

ALTER TABLE `guilds` ADD FOREIGN KEY (`leader_id`) REFERENCES `users` (`id`);

ALTER TABLE `tournament_registrations` ADD FOREIGN KEY (`tournament_id`) REFERENCES `tournaments` (`id`);

ALTER TABLE `tournament_registrations` ADD FOREIGN KEY (`participant_id`) REFERENCES `users` (`id`);

ALTER TABLE `tournament_registrations` ADD FOREIGN KEY (`participant_id`) REFERENCES `teams` (`id`);
