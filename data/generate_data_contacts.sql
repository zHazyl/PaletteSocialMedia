DROP TABLE IF EXISTS contacts;
CREATE TABLE contacts (
  user1 INTEGER NOT NULL,
  user2 INTEGER NOT NULL,
  PRIMARY KEY (`user1`,`user2`),
  FOREIGN KEY (`user1`) REFERENCES `users` (`user_id`),
  FOREIGN KEY (`user2`) REFERENCES `users` (`user_id`)
);
insert into contacts (user1, user2) values (1, 2);
insert into contacts (user1, user2) values (1, 3);
insert into contacts (user1, user2) values (1, 4);
insert into contacts (user1, user2) values (1, 5);
insert into contacts (user1, user2) values (1, 6);
insert into contacts (user1, user2) values (2, 1);
insert into contacts (user1, user2) values (2, 3);
insert into contacts (user1, user2) values (2, 4);
insert into contacts (user1, user2) values (2, 5);
insert into contacts (user1, user2) values (2, 6);
insert into contacts (user1, user2) values (3, 1);
insert into contacts (user1, user2) values (3, 2);
insert into contacts (user1, user2) values (3, 4);
insert into contacts (user1, user2) values (3, 5);
insert into contacts (user1, user2) values (3, 6);
insert into contacts (user1, user2) values (4, 1);
insert into contacts (user1, user2) values (4, 2);
insert into contacts (user1, user2) values (4, 3);
insert into contacts (user1, user2) values (4, 5);
insert into contacts (user1, user2) values (4, 6);
insert into contacts (user1, user2) values (5, 1);
insert into contacts (user1, user2) values (5, 2);
insert into contacts (user1, user2) values (5, 3);
insert into contacts (user1, user2) values (5, 4);
insert into contacts (user1, user2) values (5, 6);
insert into contacts (user1, user2) values (6, 1);
insert into contacts (user1, user2) values (6, 2);
insert into contacts (user1, user2) values (6, 3);
insert into contacts (user1, user2) values (6, 4);
insert into contacts (user1, user2) values (6, 5);