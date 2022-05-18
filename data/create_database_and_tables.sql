DROP DATABASE social_media;
CREATE DATABASE social_media;
USE social_media;


DROP TABLE IF EXISTS users;
CREATE TABLE users (
    user_id INTEGER AUTO_INCREMENT PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    username NVARCHAR(255) UNIQUE NOT NULL,
    phone NVARCHAR(255) UNIQUE NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    gender NVARCHAR(255) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    profile_photo_url NVARCHAR(255) DEFAULT 'https://picsum.photos/100',
    bio NVARCHAR(255),
    created_at TIMESTAMP DEFAULT NOW()
);

DROP TABLE IF EXISTS photos;
CREATE TABLE photos (
    photo_id INTEGER AUTO_INCREMENT PRIMARY KEY,
    photo_url NVARCHAR(255) NOT NULL UNIQUE,
    post_id	INTEGER NOT NULL,
    created_at TIMESTAMP DEFAULT NOW()
);

DROP TABLE IF EXISTS contacts;
CREATE TABLE contacts (
  user1 INTEGER NOT NULL,
  user2 INTEGER NOT NULL,
  PRIMARY KEY (`user1`,`user2`),
  FOREIGN KEY (`user1`) REFERENCES `users` (`user_id`),
  FOREIGN KEY (`user2`) REFERENCES `users` (`user_id`)
);

DROP TABLE IF EXISTS messages;
CREATE TABLE messages (
  message_id INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
  send_id INTEGER NOT NULL,
  recieve_id INTEGER NOT NULL,
  message_text nvarchar(1000) DEFAULT NULL,
  time timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (recieve_id) REFERENCES users(user_id),
  FOREIGN KEY (send_id) REFERENCES users(user_id)
);

DROP TABLE IF EXISTS post;
CREATE TABLE post (
	post_id INTEGER AUTO_INCREMENT PRIMARY KEY,
    user_id INTEGER NOT NULL,
    caption NVARCHAR(255),
    location NVARCHAR(255) ,
    created_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY(user_id) REFERENCES users(user_id)
);

DROP TABLE IF EXISTS comments;
CREATE TABLE comments (
    comment_id INTEGER AUTO_INCREMENT PRIMARY KEY,
    comment_text NVARCHAR(255) NOT NULL,
    post_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,
    created_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY(post_id) REFERENCES post(post_id),
    FOREIGN KEY(user_id) REFERENCES users(user_id)
);

DROP TABLE IF EXISTS post_likes;
CREATE TABLE post_likes (
    user_id INTEGER NOT NULL,
    post_id INTEGER NOT NULL,
    created_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY(user_id) REFERENCES users(user_id),
    FOREIGN KEY(post_id) REFERENCES post(post_id),
    PRIMARY KEY(user_id, post_id)
);

DROP TABLE IF EXISTS comment_likes;
CREATE TABLE comment_likes (
    user_id INTEGER NOT NULL,
    comment_id INTEGER NOT NULL,
    created_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY(user_id) REFERENCES users(user_id),
    FOREIGN KEY(comment_id) REFERENCES comments(comment_id),
    PRIMARY KEY(user_id, comment_id)
);

DROP TABLE IF EXISTS follows;
CREATE TABLE follows (
    follower_id INTEGER NOT NULL,
    followee_id INTEGER NOT NULL,
    created_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY(follower_id) REFERENCES users(user_id),
    FOREIGN KEY(followee_id) REFERENCES users(user_id),
    PRIMARY KEY(follower_id, followee_id)
);
