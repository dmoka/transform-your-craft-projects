use domain::{FileReader, QuerySearcher};
use std::{env, fs};

pub struct SystemFileReader;

impl FileReader for SystemFileReader {
    fn read_to_string(&self, file_path: String) -> Result<String, String> {
        fs::read_to_string(file_path).map_err(|err| err.to_string())
    }
}

fn main() {
    let args: Vec<String> = env::args().collect();

    let config = parse_config(args);

    let lines_result =
        QuerySearcher::new(SystemFileReader).search(&config.query, &config.file_path);

    print_lines(lines_result);
}

fn parse_config(args: Vec<String>) -> Config {
    Config::new(args).unwrap_or_else(|err| {
        eprintln!("Parsing arguments error: {}", err);
        std::process::exit(1);
    })
}

fn print_lines(lines_result: Result<Vec<(usize, String)>, String>) {
    match lines_result {
        Ok(lines) => {
            lines
                .iter()
                .for_each(|(line_nr, line)| println!("{}: {}", line_nr, line));
        }
        Err(err) => {
            eprintln!("Application error: {}", err);
            std::process::exit(1);
        }
    }
}

pub struct Config {
    query: String,
    file_path: String,
}

impl Config {
    pub fn new(args: Vec<String>) -> Result<Config, String> {
        if args.len() < 3 {
            return Err("not enough arguments specified".to_string());
        }

        let query = args[1].clone();
        let file_path = args[2].clone();

        Ok(Config { query, file_path })
    }
}
