pub trait FileReader {
    fn read_to_string(&self, file_path: String) -> Result<String, String>;
}

pub struct QuerySearcher<F: FileReader> {
    file_reader: F,
}

impl<F: FileReader> QuerySearcher<F> {
    pub fn new(file_reader: F) -> Self {
        QuerySearcher { file_reader }
    }

    pub fn search(&self, query: &str, file_path: &str) -> Result<Vec<(usize, String)>, String> {
        let content = self.file_reader.read_to_string(file_path.to_string())?;

        let mut matched_lines = vec![];

        for (line_index, line) in content.lines().enumerate() {
            if line.to_lowercase().contains(&query.to_lowercase()) {
                matched_lines.push((line_index + 1, line.to_string()));
            }
        }

        Ok(matched_lines)
    }
}
