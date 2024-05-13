use domain::{FileReader, QuerySearcher};

#[test]
fn should_return_single_line_when_matched_with_single_line() {
    let query = "TDD";
    let content = "TDD rocks";

    let lines = search(&query, &content);

    assert_eq!(lines, vec![(1, "TDD rocks".to_string())]);
}

#[test]
fn should_return_single_line_when_matched_with_another_single_line() {
    let query = "TDD";
    let content = "Life tastes great with TDD";

    let lines = search(&query, &content);

    assert_eq!(lines, vec![(1, "Life tastes great with TDD".to_string())]);
}

#[test]
fn should_return_no_line_when_matched_with_none() {
    let query = "pizza";
    let content = "TDD rocks";

    let lines = search(&query, &content);

    assert!(lines.is_empty());
}

#[test]
fn should_return_single_line_when_matched_with_second_line() {
    let query = "TDD";
    let content = "Life tastes great\nTDD rocks";

    let lines = search(&query, &content);

    assert_eq!(lines, vec![(2, "TDD rocks".to_string())]);
}

#[test]
fn should_return_multiple_lines_when_matched_with_multiple() {
    let query = "TDD";
    let content = "Life tastes great\nTDD rocks\nClean Code is mandatory\nIn TDD we always start writing with a failing test";

    let lines = search(&query, &content);

    assert_eq!(
        lines,
        vec![
            (2, "TDD rocks".to_string()),
            (
                4,
                "In TDD we always start writing with a failing test".to_string()
            )
        ]
    );
}

#[test]
fn should_return_single_line_when_matched_with_single_line_with_different_case() {
    let query = "tdd";
    let content = "TDD rocks";

    let lines = search(&query, &content);

    assert_eq!(lines, vec![(1, "TDD rocks".to_string())]);
}

pub struct FileReaderMock {
    lines: String,
}

impl FileReader for FileReaderMock {
    fn read_to_string(&self, _file_path: String) -> Result<String, String> {
        Ok(self.lines.clone())
    }
}

fn search(query: &str, content: &str) -> Vec<(usize, String)> {
    let file_reader_mock = FileReaderMock {
        lines: content.to_string(),
    };
    let lines = QuerySearcher::new(file_reader_mock)
        .search(&query, &content)
        .unwrap();
    lines
}
